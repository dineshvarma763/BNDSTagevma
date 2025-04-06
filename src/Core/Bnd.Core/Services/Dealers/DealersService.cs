namespace Bnd.Core.Services.Dealers
{
    using AutoMapper;
    using Bnd.Core.Extensions;
    using Bnd.Core.Helpers;
    using Bnd.Core.Models;
    using Bnd.DTO.Models;
    using Microsoft.Extensions.Configuration;
    using System.Collections.Generic;
    using Umbraco.Cms.Web.Common.PublishedModels;
    using Umbraco.Cms.Core.Web;
    using Umbraco.Extensions;
    using Bnd.Core.Services.Cache;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;
    using Bnd.DTO.Models.Cache;


    public class DealersService : IDealersService
    {
        private readonly IUmbracoContextAccessor _umbracoContextAccessor;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        private readonly ICacheService _cacheService;
        private readonly ILogger<DealersService> _logger;
        public DealersService(IUmbracoContextAccessor umbracoContextAccessor, IMapper mapper, IConfiguration config, ICacheService cacheService, ILogger<DealersService> logger)
        {
            _umbracoContextAccessor = umbracoContextAccessor;
            _mapper = mapper;
            _config = config;
            _cacheService = cacheService;
            _logger = logger;

        }

        public async Task<IEnumerable<DealerDto>?> GetAllDealers(double lat, double lng, bool freeMeasureQuote, bool serviceOrRepair, bool commercial)
        {
            var currentLocationState = await DealerHelpers.GetPostcodeFromLatLngAsync(lat, lng);
            if (currentLocationState == null)
            {
                return null;
            }
            var postcode = currentLocationState.DealerPostCode;

            if (postcode == null)
                return null;

            //*** Get Cached Code Start**************/
            var cacheKey = $"{postcode}_{freeMeasureQuote}_{serviceOrRepair}_{commercial}";

            // Try to get cached data
            var cachedData = await _cacheService.GetCache(cacheKey);

            if (!string.IsNullOrEmpty(cachedData))
            {
                //_cacheService.PurgeCache(cacheKey);
                _logger.LogInformation($"Cache hit for key: {cacheKey}");
                var result = JsonConvert.DeserializeObject<IEnumerable<DealerDto>>(cachedData);
                return result;
            }

            //*** Get Cached Code End**************/
            var dealers = await GetDealersAsync(lat, lng, freeMeasureQuote, serviceOrRepair, commercial);
            var dealersWithDistanceInfo = dealers;
            if (dealers != null)
                dealersWithDistanceInfo = await DealerHelpers.GetDistanceInfo(dealers, lat, lng);

            var mobileShowRooms = GetMobileShowroomsDealers(postcode, lat, lng, freeMeasureQuote, serviceOrRepair, commercial);

            var finalResult = dealersWithDistanceInfo.Concat(mobileShowRooms.Result)
                              .Where(dto => dto.State == currentLocationState.State)
                              .Where(dto => dto.DealerDistance <= 100)
                              .OrderByDescending(dto => dto.IsMobileShowroom)
                              .ThenBy(dto => dto.DealerDistance)
                              .ToList();

            if (finalResult is null || !finalResult.Any())
                return null;

            var dealerResult = _mapper.Map<IEnumerable<DealerDto>>(finalResult);

            await _cacheService.UpdateCache(new DealerCache
            {
                CacheKey = cacheKey,
                Dealers = dealerResult.ToList()
            });
            _logger.LogInformation($"Cache updated for key: {cacheKey}");
            // Cache the result end

            return dealerResult;

        }

        public IEnumerable<DealerDto>? SearchDealers(string searchTerm)
        {
            var dealers = GetDealers();
            if (dealers is null)
                return null;


            var result = _mapper.Map<IEnumerable<DealerDto>>(dealers).Where(d => !d.MobileHubShowroom);

            // perform search
            if (!string.IsNullOrEmpty(searchTerm))
            {
                result = result.OrderByDealer(searchTerm);
            }

            return result;
        }

        public IEnumerable<DealerDto>? SearchDealers(string searchTerm, double latitude, double longitude, bool freeMeasureQuote, bool serviceOrRepair, bool commercial)
        {
            var dealers = GetDealers(latitude, longitude, freeMeasureQuote, serviceOrRepair, commercial);

            if (dealers is null)
                return null;

            var result = _mapper.Map<IEnumerable<DealerDto>>(dealers).Where(d => !d.MobileHubShowroom);

            // perform search
            if (!string.IsNullOrEmpty(searchTerm))
            {
                result = result.OrderByDealer(searchTerm);
            }

            return result;
        }

        public IEnumerable<string>? GetSuggestedDealers(string searchTerm)
        {
            var dealers = GetDealers();
            if (dealers is null)
                return null;

            var dealerResult = _mapper.Map<IEnumerable<DealerDto>>(dealers);

            // perform search
            if (!string.IsNullOrEmpty(searchTerm))
            {
                dealerResult = dealerResult.OrderByDealer(searchTerm);
            }

            var result = dealerResult.Select(d => d.Address);
            return result;
        }

        private IEnumerable<Dealer>? GetDealers()
        {
            var dealersFolder = _umbracoContextAccessor.GetRequiredUmbracoContext().Content?.GetByXPath(Constants.DealersFolderXPath).First();
            if (dealersFolder is null)
                return null;

            var dealers = dealersFolder.Children.Cast<Dealer>();

            return dealers;
        }


        private async Task<IEnumerable<DealerDto>?> GetDealersAsync(double latitude, double longitude, bool freeMeasureQuote, bool serviceOrRepair, bool commercial)
        {
            var umbracoContext = _umbracoContextAccessor.GetRequiredUmbracoContext();
            var dealersFolder = umbracoContext.Content?.GetByXPath(Constants.DealersFolderXPath)?.First();
            var defaultSearchSetting = umbracoContext.Content?.GetByXPath(Constants.DefaultSearchSetting)?.First();
            var defaultKiloMeter = defaultSearchSetting.Value<string>("radiusRangeMeters");

            if (dealersFolder is null)
                return null;

            var cacheKey = $"dealers_{freeMeasureQuote}_{serviceOrRepair}_{commercial}";
            var cachedData = await _cacheService.GetCache(cacheKey);
            List<DealerDto> dealersAll;

            if (!string.IsNullOrEmpty(cachedData))
            {
                dealersAll = JsonConvert.DeserializeObject<List<DealerDto>>(cachedData);
                return dealersAll;

            }
            else
            {

                var dAll = dealersFolder.Children.Cast<Dealer>()
                                    .Where(w => w.MobileHubShowroom == false &&
                                                (!freeMeasureQuote || w.FreeMeasureQuote) &&
                                                (!serviceOrRepair || w.ServiceOrRepair) &&
                                                (!commercial || w.Commercial))
                                    .ToList();
                dealersAll = _mapper.Map<List<DealerDto>>(dAll);

                var tasks = dealersAll.Select(async dealer =>
                {

                    var storeLat = dealer.Latitude;
                    var storeLong = dealer.Longitude;
                    var dealerLatLngDistance = new LatLngDistanceDTO();

                    if (!dealer.DistanceCalculateFromLatLng && dealer.Address != null)
                    {
                        dealerLatLngDistance = await DealerHelpers.UpdateLatLngFromAddressAsync(dealer.Address);
                        dealer.Latitude = dealerLatLngDistance.Latitude;
                        dealer.DealerDistance = dealerLatLngDistance.DealerDistance;
                        dealer.Latitude = dealerLatLngDistance.Latitude;
                        dealer.Longitude = dealerLatLngDistance.Longitude;
                        dealer.DealerPostCode = dealerLatLngDistance.DealerPostCode;
                        dealer.State = dealerLatLngDistance.State;
                    }
                    return dealer;
                });

                var mytask = await Task.WhenAll(tasks);
                // Cache the data for future use
                await _cacheService.UpdateCache(new DealerCache
                {
                    CacheKey = $"dealers_{freeMeasureQuote}_{serviceOrRepair}_{commercial}",
                    Dealers = _mapper.Map<IEnumerable<DealerDto>>(mytask).ToList(),
                });

                return mytask;
            }
        }


        private async Task<IEnumerable<DealerDto>?> GetDealers(double? latitude, double? longitude, bool freeMeasureQuote, bool serviceOrRepair, bool commercial)
        {
            var dealersFolder = _umbracoContextAccessor.GetRequiredUmbracoContext().Content?.GetByXPath(Constants.DealersFolderXPath).First();
            var defaultSearchSetting = _umbracoContextAccessor.GetRequiredUmbracoContext().Content?.GetByXPath(Constants.DefaultSearchSetting).First();
            var defaultKiloMeter = defaultSearchSetting.Value<string>("radiusRangeMeters");

            if (dealersFolder is null)
                return null;

            var dealersAll = dealersFolder.Children.Cast<Dealer>().
                                Where(w => w.MobileHubShowroom == false &&
                                           (!freeMeasureQuote || w.FreeMeasureQuote) &&
                                                (!serviceOrRepair || w.ServiceOrRepair) &&
                                           (!commercial || w.Commercial)
                                    )
                                    .ToList();

            var dealers = _mapper.Map<IEnumerable<DealerDto>>(dealersAll);
            var finalDealers = new List<DealerDto>();

            foreach (var dealer in dealers)
            {
                var storeLat = dealer.Latitude;
                var storeLong = dealer.Longitude;
                var dealerLatLngDistance = new LatLngDistanceDTO();
                if (dealer.DistanceCalculateFromLatLng)
                {
                    dealerLatLngDistance = await DealerHelpers.GetDistanceInKMFromLatLngAsync(Convert.ToDouble(latitude), Convert.ToDouble(longitude), Convert.ToDouble(storeLat), Convert.ToDouble(storeLong));
                }
                else
                {
                    dealerLatLngDistance = await DealerHelpers.GetDistanceInKMFromAddressAsync(Convert.ToDouble(latitude), Convert.ToDouble(longitude), dealer.Address);
                }

                var distanceKM = defaultKiloMeter == default ? 30 : Convert.ToInt32(defaultKiloMeter);
                dealer.DealerDistance = dealerLatLngDistance.DealerDistance;
                dealer.Latitude = dealerLatLngDistance.Latitude;
                dealer.Longitude = dealerLatLngDistance.Longitude;
                dealer.DealerPostCode = dealerLatLngDistance.DealerPostCode;
                dealer.State = dealerLatLngDistance.State;
                var distance = dealerLatLngDistance.DealerDistance;
                finalDealers.Add(dealer);
            }
            return finalDealers;

        }

        public async Task<IEnumerable<DealerDto>?> GetMobileShowroomsDealers(string address, double latitude, double longitude, bool freeMeasureQuote, bool serviceOrRepair, bool commercial)
        {
            var mobileShowrromsFolder = _umbracoContextAccessor.GetRequiredUmbracoContext().Content?.GetByXPath(Constants.MobileShowroomsXPath).First();
            var defaultSearchSetting = _umbracoContextAccessor.GetRequiredUmbracoContext().Content?.GetByXPath(Constants.DefaultSearchSetting).First();
            var defaultKiloMeter = defaultSearchSetting.Value<string>("radiusRangeMeters");

            if (mobileShowrromsFolder is null)
                return null;

            var mobileShowroomsAll = mobileShowrromsFolder.Children
                                    .Cast<MobileShowroomItem>()
                                    .Where(w =>
                                        w.PostcodeList.Split('|').ToList().Contains(address) &&
                                        (!freeMeasureQuote || w.FreeMeasureQuote) &&
                                        (!serviceOrRepair || w.ServiceOrRepair) &&
                                        (!commercial || w.Commercial)
                                    )
                                    .ToList();


            var mobileShowrooms = _mapper.Map<IEnumerable<DealerDto>>(mobileShowroomsAll);
            var finalMobileShowrooms = new List<DealerDto>();

            var findDealerDealerLink = _umbracoContextAccessor.GetRequiredUmbracoContext().Content?.GetByRoute(Constants.DealerNearMeFindDealerRoute);
            var bookNowLink = _mapper.Map<DTO.Models.CtaDto>(findDealerDealerLink);

            var tasks = mobileShowrooms.Select(async mobileShowroom =>
            {
                var dealerLatLngDistance = await DealerHelpers.GetDistanceInKMFromAddressAsync(latitude, longitude, mobileShowroom.Postcode.ToString());
                mobileShowroom.DealerDistance = dealerLatLngDistance.DealerDistance;
                mobileShowroom.Latitude = dealerLatLngDistance.Latitude;
                mobileShowroom.Longitude = dealerLatLngDistance.Longitude;
                mobileShowroom.DealerPostCode = dealerLatLngDistance.DealerPostCode;
                mobileShowroom.State = dealerLatLngDistance.State;
                mobileShowroom.IsMobileShowroom = true;
                mobileShowroom.BookNow = bookNowLink;
                return mobileShowroom;
            });
            return await Task.WhenAll(tasks);
        }

        public async Task<IEnumerable<DealerDto>?> GetMobileShowrooms(double lat, double lng, bool freeMeasureQuote, bool serviceOrRepair, bool commercial)
        {
            var currentLocationState = await DealerHelpers.GetPostcodeFromLatLngAsync(lat, lng);
            var postcode = currentLocationState.DealerPostCode;

            if (currentLocationState == null)
            {
                return null;
            }

            //*** Get Cached Code Start**************/
            var cacheKey = $"{postcode}_{freeMeasureQuote}_{serviceOrRepair}_{commercial}";

            // Try to get cached data
            var cachedData = await _cacheService.GetCache(cacheKey);

            if (!string.IsNullOrEmpty(cachedData))
            {
                //_cacheService.PurgeCache(cacheKey);
                _logger.LogInformation($"Cache hit for key: {cacheKey}");
                var result = JsonConvert.DeserializeObject<IEnumerable<DealerDto>>(cachedData);
                return result;
            }

            return await GetMobileShowroomsDealers(postcode, lat, lng, freeMeasureQuote, serviceOrRepair, commercial);
        }

        private IEnumerable<MobileShowroomItem>? GetShowrooms()
        {
            var mobileShowrromsFolder = _umbracoContextAccessor.GetRequiredUmbracoContext().Content?.GetByXPath(Constants.MobileShowroomsXPath).First();

            if (mobileShowrromsFolder is null)
                return null;
            var mobileShowrooms = mobileShowrromsFolder.Children.Cast<MobileShowroomItem>();

            return mobileShowrooms;
        }

    }
}
