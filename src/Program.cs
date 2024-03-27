﻿using System.Threading.Tasks;
using Statiq.Alerts;
using Statiq.App;
using Statiq.Web;

return await Bootstrapper
  .Factory
  .CreateWeb(args)
  .AddTabGroupShortCode()
  .AddIncludeCodeShortCode()
  .AddAlertShortCodes()
  .RunAsync();