using Simple_API_Assessment.Model;

namespace Simple_API_Assessment.Data.Repository;

public interface IApplicantRepository {
	Task<IEnumerable<Applicant>> GetAllApplicants();
	Task<Applicant?> GetApplicantById(long id);
	Task<bool> CreateApplicant(Applicant applicant);
	Task<bool> UpdateApplicant(Applicant applicant);
	Task<bool> DeleteApplicant(long id);
}