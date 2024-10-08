﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementApp.Models
{
    internal class Task
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Due_Date { get; set; }
        public string Status { get; set; }
    }
}
