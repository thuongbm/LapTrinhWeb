﻿using Lab1.Models;
using Microsoft.AspNetCore.Mvc;

namespace Lab1.ViewComponents
{
    public class RenderViewComponent:ViewComponent
    {
        private List<MenuItem> MenuItems = new List<MenuItem>();
        public RenderViewComponent()
        {
            MenuItems = new List<MenuItem>() {
                new MenuItem() { Id = 1, Name="Branches", Link="/Branches/Index"},
                new MenuItem() { Id = 2, Name = "Students", Link = "List" },
                new MenuItem() { Id = 3, Name = "Subjects", Link = "/Subjects/Index" },
                new MenuItem() { Id = 4, Name = "Courses", Link = "/Courses/Index" }
            };
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View("RenderLeftMenu", MenuItems);
        }
    }
}
