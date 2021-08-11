﻿namespace ThreatsManager.QuantitativeRisk.Engine
{
    /// <summary>
    /// Loss Event Frequency. The probable frequency, within a given time-frame, that loss will materialize from a threat agent's action.
    /// </summary>
    public class LossEventFrequency : Frequency
    {
        /// <summary>
        /// Threat Event Frequency. The probable frequency, within a given time-frame, that threat agents will act in a manner that may result in loss. 
        /// </summary>
        public ThreatEventFrequency ThreatEventFrequency { get; set; }

        /// <summary>
        /// Vulnerability. The probability that a threat agent's actions will result in loss.
        /// </summary>
        public Vulnerability Vulnerability { get; set; }
    }
}