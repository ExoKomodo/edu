using Client.Config;
using Client.Models.School;
using Client.Models.School.Baby;
using Client.Services;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace Client.Pages.School
{
  public partial class Index
    {
        public bool IsLoading { get; set; }
        public const bool IsDebug =
            #if DEBUG
            true
            #else
            false
            #endif
            ;
        public ICollection<Curriculum> Curricula { get; set; }

        protected override void OnInitialized()
        {
            Curricula = new List<Curriculum>
            {
                new BabyCurriculum(),
            };
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