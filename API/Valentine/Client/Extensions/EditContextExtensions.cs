using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Valentine.Client.Extensions
{
    internal static class EditContextExtensions
    {
        public static void OnPropertyChanged(this EditContext context, string propertyName)
        {
            var fieldIdentifier = context.Field(propertyName);
            context.NotifyFieldChanged(fieldIdentifier);
        }
    }
}
