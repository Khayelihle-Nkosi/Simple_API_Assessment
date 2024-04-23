namespace Simple_API_Assessment.Model;

public sealed class Skill {
	public long Id { get; set; }
	public string Name { get; set; } = null!;
	public long ApplicantId { get; set; }
	public Applicant Applicant { get; set; }  = null!;
}