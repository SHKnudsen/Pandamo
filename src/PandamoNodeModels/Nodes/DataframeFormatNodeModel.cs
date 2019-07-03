using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.DesignScript.Runtime;
using Dynamo.Controls;
using Dynamo.Graph.Nodes;
using Dynamo.Wpf;
using ProtoCore.AST.AssociativeAST;
using Newtonsoft.Json;
using DynamoPandas.Pandas;
using DynamoPandas.Format;

namespace PandamoNodeModels.Nodes
{
    [NodeName("Tabulate")]
    [NodeCategory("DataFrameFormatters.Tabulate")]
    [NodeDescription("")]
    // The InPortNames attribute determines the
    // amount of input ports of your node and their names.
    [InPortNames("Dataframe")]
    [InPortTypes("DataFrame")]
    // The InPortDescriptions attribute sets the description
    // of your input ports in the tooltip shown when you hover them.
    [InPortDescriptions("df")]

    // The OutPortNames attribute determines the
    // amount of output ports of your node and their names.
    [OutPortNames("string")]
    [OutPortTypes("string")]
    [IsDesignScriptCompatible]
    public class DataframeFormatNodeModel : NodeModel
    {
        public DataframeFormatNodeModel()
        {
            RegisterAllPorts();
        }
        [JsonConstructor]
        public DataframeFormatNodeModel(IEnumerable<PortModel> inPorts, IEnumerable<PortModel> outPorts) : base(inPorts, outPorts)
        {

        }

        /// <summary>
        /// BuildOutputAst is where the outputs of this node are calculated.
        /// This method is used to do the work that a compiler usually does 
        /// by parsing the inputs List inputAstNodes into an abstract syntax tree.
        /// </summary>
        /// <param name="inputAstNodes"></param>
        /// <returns></returns>
        [IsVisibleInDynamoLibrary(false)]
        public override IEnumerable<AssociativeNode> BuildOutputAst(List<AssociativeNode> inputAstNodes)
        {
            // WARNING!!!
            // Do not throw an exception during AST creation.

            // If inputs are not connected return null
            if (!InPorts[0].IsConnected)
            {
                return new[]
                {
                    AstFactory.BuildAssignment(GetAstIdentifierForOutputIndex(0), AstFactory.BuildNullNode()),
                };
            }

            // Build a function call to a C# function which lives in a different DLL assembly
            // and use input nodes 0 and 1 as input values in the fuction
            // Note that we specify input and output value types in new Func<double, double, double>
            // which means we have two double inputs and one double output
            AssociativeNode inputNode = AstFactory.BuildFunctionCall(
                new Func<DataFrame, string>(DataFrameFormatters.Tabulate),
                new List<AssociativeNode> { inputAstNodes[0]}
            );

            return new[]
            {
                AstFactory.BuildAssignment(GetAstIdentifierForOutputIndex(0), inputNode),
                    AstFactory.BuildAssignment(
                        AstFactory.BuildIdentifier(AstIdentifierBase + "_dummy"),
                        VMDataBridge.DataBridge.GenerateBridgeDataAst(GUID.ToString(), AstFactory.BuildExprList(inputAstNodes)
                    )
                ),
            };
        }
    }
}
