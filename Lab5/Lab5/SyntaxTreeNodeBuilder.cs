using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5
{
    internal static class SyntaxTreeNodeBuilder
    {
        private static readonly Dictionary<string, int> Operators = new()
        {
            ["("] = 1,
            [")"] = 1,
            ["**"] = 2,
            ["*"] = 3,
            ["/"] = 3,
            ["+"] = 4,
            ["-"] = 4,
        };

        public static SyntaxTreeNode BuildLineNode(string line)
        {
            LineType lineType = DiscernLineType(line);
            return lineType switch
            {
                LineType.Statement => BuildStatementNode(line),
                LineType.Assignment => BuildAssignmentNode(line),
                LineType.Expression => BuildExpressionNode(line),
                _ => throw new InvalidOperationException()
            };
        }

        private static SyntaxTreeNode BuildStatementNode(string line)
        {
            string[] splitStatement = line.Split(
                new string[] { "if(", ")=>(", ")else=>(", ")" },
                StringSplitOptions.RemoveEmptyEntries);

            string statementBody = splitStatement[0];
            string trueBlock = splitStatement[1];
            string elseBlock = splitStatement[2];

            SyntaxTreeNode statementNode = new(NodeType.IfStatement, null);

            SyntaxTreeNode conditionNode = new(NodeType.IfCondition, null);
            conditionNode.AddChild(BuildLineNode(trueBlock));

            SyntaxTreeNode trueNode = new(NodeType.IfConditionTrue, null);
            trueNode.AddChild(BuildLineNode(trueBlock));

            SyntaxTreeNode falseNode = new(NodeType.IfConditionFalse, null);
            trueNode.AddChild(BuildLineNode(elseBlock));

            statementNode.AddChild(conditionNode);
            statementNode.AddChild(trueNode);
            statementNode.AddChild(falseNode);

            return statementNode;
        }

        private static SyntaxTreeNode BuildAssignmentNode(string line)
        {
            string[] splitAssignment = line.Split("=");

            SyntaxTreeNode expressionTree = BuildExpressionNode(splitAssignment[^1]);
            SyntaxTreeNode assignmentNode = new(NodeType.Variable, new(splitAssignment[^2]));
            assignmentNode.AddChild(expressionTree);

            for (int i = splitAssignment.Length - 3; i >= 0; i--)
            {
                SyntaxTreeNode newAssignmentNode = new(NodeType.Variable, new(splitAssignment[i])); ;
                newAssignmentNode.AddChild(assignmentNode);
                assignmentNode = newAssignmentNode;
            }

            return assignmentNode;
        }

        private static SyntaxTreeNode BuildExpressionNode(string line)
        {
            string[] postfixExpression = ExpressionConverter.ConvertToPostfix(line);
            Stack<SyntaxTreeNode> nodes = new();

            foreach (string lexeme in postfixExpression)
            {
                SyntaxTreeNode newNode;

                switch (lexeme)
                {
                    case string s when double.TryParse(s, out double d):
                        newNode = new (NodeType.Value, new(d));
                        nodes.Push(newNode);
                        break;
                    case string op when Operators.ContainsKey(op):
                        Operator operation = op switch
                        {
                            "+" => Operator.Plus,
                            "-" => Operator.Minus,
                            "*" => Operator.Multiply,
                            "/" => Operator.Divide,
                            "**" => Operator.Pow,
                            _ => throw new InvalidOperationException()
                        };
                        SyntaxTreeNode rightNode = nodes.Pop();
                        SyntaxTreeNode leftNode = nodes.Pop();
                        newNode = new (
                            NodeType.Operator,
                            new(operation),
                            leftNode,
                            rightNode);
                        nodes.Push(newNode);
                        break;
                    case string variable:
                        newNode = new (NodeType.Variable, new(variable));
                        nodes.Push(newNode);
                        break;
                }
            }

            return nodes.Pop();
        }

        private static LineType DiscernLineType(string line)
        {
            if (line.Contains("if"))
            {
                return LineType.Statement;
            }

            if (line.Contains("="))
            {
                return LineType.Assignment;
            }

            return LineType.Expression;
        }
    }
}
