﻿using Hangfire.Server;
using System;

namespace Services.Hangfire
{
    public class JobContext : IServerFilter
    {
        [ThreadStatic]
        private static string _jobId;

        public static string JobId { get { return _jobId; } set { _jobId = value; } }

        public void OnPerforming(PerformingContext context)
        {
            JobId = context.JobId;
        }

        public void OnPerformed(PerformedContext filterContext)
        { }
    }
}