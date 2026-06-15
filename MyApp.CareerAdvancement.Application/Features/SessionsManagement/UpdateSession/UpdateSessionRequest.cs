using Facet;
using Facet.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.CareerAdvancement.Application.Features.SessionsManagement.UpdateSession
{
    // [Facet] generates all property mappings at compile time — zero reflection
    [Facet(typeof(UpdateSessionCommand))]
    public sealed partial class UpdateSessionRequest
    {
        public UpdateSessionCommand ToCommand(int id)
        {
            UpdateSessionCommand command = this.ToSource<UpdateSessionRequest, UpdateSessionCommand>();
            command.Id = id;   // inject route param after mapping
            return command;
        }
    }
}
