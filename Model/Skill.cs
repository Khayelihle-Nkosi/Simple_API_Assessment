using System.Text.Json.Serialization;

namespace Simple_API_Assessment.Model;

public class Skill {
	public long Id { get; set; }
	public string Name { get; set; } = null!;
	[JsonIgnore]
	public long ApplicantId { get; set; }
	[JsonIgnore]
	public Applicant? Applicant { get; set; }
}