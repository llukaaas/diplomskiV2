﻿namespace Nop.Plugin.Shipping.USPS.Domain;

/// <summary>
/// Represents error generated by the Web Tools server (see https://www.usps.com/business/web-tools-apis/rate-calculator-api.htm#_Toc525907198)
/// </summary>
public class ResponseError
{
    /// <summary>
    /// The error number generated by the Web Tools server
    /// </summary>
    public string Number { get; set; }

    /// <summary>
    /// The component and interface that generated the error on the Web Tools server
    /// </summary>
    public string Source { get; set; }

    /// <summary>
    /// The error description
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Reserved for future use
    /// </summary>
    public string HelpFile { get; set; }

    /// <summary>
    /// Reserved for future use
    /// </summary>
    public string HelpContext { get; set; }
}
