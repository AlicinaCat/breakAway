﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace BreakAway.Models.Contact
{
    public class EditViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
        public DateTime AddDate { get; set; }
        public DateTime ModifiedDate { get; set; }

    }
}