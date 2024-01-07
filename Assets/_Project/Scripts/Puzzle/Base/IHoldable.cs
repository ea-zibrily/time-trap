namespace TimeTrap.Puzzle
{
    public interface IHoldable
    {
        public void OnHolded();
        public bool IsCorrect();
    }
}