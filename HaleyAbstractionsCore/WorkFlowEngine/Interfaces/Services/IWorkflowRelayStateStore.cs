using System.Threading;
using System.Threading.Tasks;

namespace Haley.Abstractions {
    /// <summary>
    /// Persists the current relay state per workflow entity.
    /// Applications can replace the default in-memory implementation with a database store
    /// when relay execution must pause for human actions and resume later.
    /// </summary>
    public interface IWorkflowRelayStateStore {
        Task<string?> GetCurrentStateAsync(string workflowName, string entityId, int envCode = 0, CancellationToken ct = default);
        Task SaveCurrentStateAsync(string workflowName, string entityId, int envCode, string currentState, CancellationToken ct = default);
    }
}
