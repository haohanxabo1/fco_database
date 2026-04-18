using System.ComponentModel.DataAnnotations;

namespace fco_database.Models;

public class SeasonCardModel
{
    
    public string DataId { get; set; }
    [Key]
    public string Uid { get; set; }
    public string Season { get; set; }
    public int Salary { get; set; }
    public string Foot { get; set; }
    public int Weak_foot { get; set; }
    public string Reputation { get; set; }
    public string Workrate_att { get; set; }
    public string Workrate_def { get; set; }
    public int Ovr { get; set; }
    public int St { get; set; }
    public int Rw { get; set; }
    public int Cf { get; set; }
    public int Cam { get; set; }
    public int Rm { get; set; }
    public int Cm { get; set; }
    public int Cdm { get; set; }
    public int Rwb { get; set; }
    public int Rb { get; set; }
    public int Cb { get; set; }
    public int Gk { get; set; }
    public int IsActive { get; set; }
}