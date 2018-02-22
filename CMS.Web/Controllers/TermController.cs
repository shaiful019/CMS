﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CMS.Domain.Models;
using CMS.Core.Interfaces;
using CMS.Domain.ViewModels;
using Microsoft.AspNetCore.Authorization;


namespace CMS.Web.Controllers
{
    //[Authorize]
    public class TermController : Controller
    {


        private ITermService termService;
        
        public TermController(ITermService _termService)
        {
            termService = _termService;           
        }


        public ActionResult Index()
        {
            var item = termService.GetAllTerm();
            return View(item);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(TermViewModel termVM)
        {

            termService.Create(termVM);
            

            return View();
        }

        public ActionResult Edit(int id)
        {
            var term = termService.GetTermByID(id);

            return View(term);
        }

        [HttpPost]
        public ActionResult Edit(TermViewModel termVM)
        {
            
            termService.Update(termVM);

            return View();
        }
    }
}
