﻿#region Using Statements
    using System;

    using Cake.Core;

    using Amazon;
    using Amazon.Runtime;
#endregion



namespace Cake.AWS.Route53
{
    /// <summary>
    /// Contains extension methods for <see cref="ICakeEnvironment" />.
    /// </summary>
    public static class CakeEnvironmentExtensions
    {
        /// <summary>
        /// Helper method to get the AWS Credentials from environment variables
        /// </summary>
        /// <param name="environment">The cake environment.</param>
        /// <returns>A new <see cref="Route53Settings"/> instance to be used in calls to the <see cref="IRoute53Manager"/>.</returns>
        public static Route53Settings CreateRoute53Settings(this ICakeEnvironment environment)
        {
            if (environment == null)
            {
                throw new ArgumentNullException("environment");
            }

            Route53Settings settings = new Route53Settings();
            
            //AWS Fallback
            AWSCredentials creds = FallbackCredentialsFactory.GetCredentials();
            if (creds != null)
            {
                settings.Credentials = creds;
            }

            //Environment Variables
            string region = environment.GetEnvironmentVariable("AWS_REGION");
            if (!String.IsNullOrEmpty(region))
            {
                settings.Region = RegionEndpoint.GetBySystemName(region);
            }

            return settings;
        }
    }
}
