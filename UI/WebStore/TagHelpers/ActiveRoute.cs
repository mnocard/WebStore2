﻿using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WebStore.TagHelpers
{
    [HtmlTargetElement(Attributes = AttributeName)]
    public class ActiveRoute :TagHelper
    {
        private const string AttributeName = "is-active-route";

        [HtmlAttributeName("asp-action")]
        public string Action { get; set; }
        
        [HtmlAttributeName("asp-controller")]
        public string Controller { get; set; }

        [HtmlAttributeName("asp-all-route-data", DictionaryAttributePrefix = "asp-route-")]
        public Dictionary<string, string> RouteValues { get; set; } = new(StringComparer.OrdinalIgnoreCase);

        [ViewContext, HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (IsActive())
                MakeActive(output);
            output.Attributes.RemoveAll(AttributeName);
        }

        /// <summary>
        /// Проверяет включена ли ссылка или нет
        /// </summary>
        /// <returns></returns>
        private bool IsActive()
        {
            var route_values = ViewContext.RouteData.Values;

            var current_controller = route_values["controller"]?.ToString();
            var current_action = route_values["action"]?.ToString();

            const StringComparison str_cmp = StringComparison.OrdinalIgnoreCase;

            if (!string.IsNullOrEmpty(Controller) && !string.Equals(current_controller, Controller, str_cmp))
                return false;

            if (!string.IsNullOrEmpty(Action) && !string.Equals(current_action, Action, str_cmp))
                return false;

            foreach (var (key, value) in RouteValues)
            {
                if (!route_values.ContainsKey(key) || route_values[key]?.ToString() != value)
                    return false;
            }

            return true;
        }

        private static void MakeActive(TagHelperOutput output)
        {
            var class_attribute = output.Attributes.FirstOrDefault(a => a.Name == "class");

            if (class_attribute is null)
                output.Attributes.Add("class", "active");
            else
            {
                if (class_attribute.Value.ToString()?.Contains("active") ?? false)
                    return;

                output.Attributes.SetAttribute("class", class_attribute.Value + " active");
            }
        }
    }
}
