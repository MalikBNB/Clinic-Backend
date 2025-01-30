using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Entities.DTOs.BaseDtos;
public class PersonDto
{
    [Required]
    [MinLength(2, ErrorMessage = "Firstname cannot be less than 2 characters")]
    [MaxLength(50, ErrorMessage = "Firstname cannot be more than 50 characters")]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    [MinLength(2, ErrorMessage = "Firstname cannot be less than 2 characters")]
    [MaxLength(50, ErrorMessage = "Firstname cannot be more than 50 characters")]
    public string LastName { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    public DateTime DateOfBirth { get; set; }

    public byte Status { get; set; }
}
