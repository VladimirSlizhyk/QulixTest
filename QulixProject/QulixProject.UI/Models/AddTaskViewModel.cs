using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using QulixProject.Core.Entities;

namespace QulixProject.UI.Models
{
    public class AddTaskViewModel
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Имя")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Объём работы")]
        public int Workload { get; set; }
        [Required]
        [Display(Name = "Дата начала")]
        public DateTime StartDate { get; set; }
        [Required]
        [Display(Name = "Дата окончания")]
        public DateTime EndDate { get; set; }
        [Required]
        [Display(Name = "Статус")]
        public Status Status { get; set; }
        [Required]
        [Display(Name = "Исполнитель")]
        public int PerformerId { get; set; }
        public List<Performer> PerformerModels { get; set; }
    }
}