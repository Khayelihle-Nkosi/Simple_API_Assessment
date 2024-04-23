namespace Simple_API_Assessment.Model;

public class Applicant {
	public long Id { get; set; }
	public string Name { get; set; } = null!;
	public IEnumerable<Skill>? Skills { get; set; }
}