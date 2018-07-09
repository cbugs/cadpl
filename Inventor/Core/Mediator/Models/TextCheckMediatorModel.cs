namespace Cadbury.Inventor.Core.Mediator.Models
{
    public class TextCheckMediatorModel : IMediatorModel
    {
        public string Text { get; set; }
        public WordTree.WordTree WordTree { get; set; }
    }
}
