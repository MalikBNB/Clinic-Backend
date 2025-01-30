using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Entities.DTOs.BaseDtos;
public class UpdatePersonDto
{
    [Required]
    public DateTime DateOfBirth { get; set; }

    [Required]
    [MinLength(4, ErrorMessage = "Gendor cannot be less than 4 characters")]
    [MaxLength(6, ErrorMessage = "Gendor cannot be more than 6 characters")]
    public string Gendor { get; set; } = string.Empty;

    [Required]
    [Phone]
    public string Phone { get; set; } = string.Empty;

    [Required]
    [MinLength(10, ErrorMessage = "Address cannot be less than 10 characters")]
    [MaxLength(250, ErrorMessage = "Address cannot be more than 250 characters")]
    public string Address { get; set; } = string.Empty;

}
