using Client.Config;
using Client.Models.School.Baby;
using Client.Services;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace Client.Pages.School.Baby
{
  public partial class Index
    {
        public const bool IsDebug =
            #if DEBUG
            true
            #else
            false
            #endif
            ;
        public BabyCurriculum Curriculum { get; set; }

        protected override void OnInitialized()
        {
            Curriculum = new BabyCurriculum();
        }

        protected override void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                AppState.Reset();
            }
        }
    }
}