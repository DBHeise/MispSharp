
namespace Misp.Cmdlet
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Management.Automation;

    [Cmdlet(VerbsCommon.Get, "MispEvents", SupportsShouldProcess = true)]
    [OutputType(typeof(Misp.MispEvent[]))]
    public class GetMispEvents : PSCmdlet
    {
        private MispServer server;
        public GetMispEvents() : base() { }

        [Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true, HelpMessage = "MISP server instance")]
        public String Server { get; set; }

        [Parameter(Mandatory = true, HelpMessage = "Authentication Key")]
        public String Key { get; set; }

        protected override void BeginProcessing()
        {
            this.server = new MispServer(this.Server, this.Key);
            base.BeginProcessing();
        }

        protected override void ProcessRecord()
        {
            WriteObject(this.server.GetEvents());
        }
    }
}
