using System.Collections.Generic;
using System.Threading.Tasks;
using ComPact.Models;

namespace ComPact.WebServices
{
	public interface IApiService
	{
		Task<Assignment> AddAssignment(Assignment assignment);
		Task<Assignment> UpdateAssignment(Assignment assignment);

		Task<User> AddUser(User user);
		Task<User> LoginUser(User user);
		Task<User> GetUser(string email);

		Task<Member> AddMember(Member member);
		Task<Member> GetMember(string adminId);
		Task<IEnumerable<Member>> GetMembers(string adminId);
		Task<IEnumerable<Assignment>> GetAssignments(string userId, bool isAdmin);
		Task<bool> DeleteAssignment(string assignmentId);

		Task<Payment> AddPayment(Payment payment);
		Task<IEnumerable<Payment>> GetPayments(string userId, bool isAdmin);
		Task<bool> DeletePayment(string paymentId);
		Task<Payment> UpdatePayment(Payment assignment);
	}
}