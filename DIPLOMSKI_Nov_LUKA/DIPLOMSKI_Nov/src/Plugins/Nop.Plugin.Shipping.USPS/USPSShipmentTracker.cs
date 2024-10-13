﻿using Nop.Core.Domain.Shipping;
using Nop.Plugin.Shipping.USPS.Services;
using Nop.Services.Shipping.Tracking;

namespace Nop.Plugin.Shipping.USPS;

public class USPSShipmentTracker : IShipmentTracker
{
    #region Fields

    private readonly USPSService _uspsService;

    #endregion

    #region Ctor

    public USPSShipmentTracker(USPSService uspsService)
    {
        _uspsService = uspsService;
    }

    #endregion

    #region Methods

    /// <summary>
    /// Get all shipment events
    /// </summary>
    /// <param name="trackingNumber">The tracking number to track</param>
    /// <param name="shipment">Shipment; pass null if the tracking number is not associated with a specific shipment</param>
    /// <returns>
    /// A task that represents the asynchronous operation
    /// The task result contains the list of shipment events
    /// </returns>
    public async Task<IList<ShipmentStatusEvent>> GetShipmentEventsAsync(string trackingNumber, Shipment shipment = null)
    {
        var result = new List<ShipmentStatusEvent>();

        if (string.IsNullOrEmpty(trackingNumber))
            return result;

        result.AddRange(await _uspsService.GetShipmentEventsAsync(trackingNumber));

        return result;
    }

    /// <summary>
    /// Get URL for a page to show tracking info (third party tracking page)
    /// </summary>
    /// <param name="trackingNumber">The tracking number to track</param>
    /// <param name="shipment">Shipment; pass null if the tracking number is not associated with a specific shipment</param>
    /// <returns>
    /// A task that represents the asynchronous operation
    /// The task result contains the URL of a tracking page
    /// </returns>
    public Task<string> GetUrlAsync(string trackingNumber, Shipment shipment = null)
    {
        return Task.FromResult($"https://tools.usps.com/go/TrackConfirmAction?tLabels={trackingNumber}");
    }

    /// <summary>
    /// Gets if the current tracker can track the tracking number.
    /// </summary>
    /// <param name="trackingNumber">The tracking number to track.</param>
    /// <returns>True if the tracker can track, otherwise false.</returns>
    public Task<bool> IsMatchAsync(string trackingNumber)
    {
        if (string.IsNullOrWhiteSpace(trackingNumber))
            return Task.FromResult(false);

        //What is a FedEx tracking number format?
        return Task.FromResult(false);
    }

    #endregion
}
