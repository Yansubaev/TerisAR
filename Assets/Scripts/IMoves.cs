namespace Tetris
{
    public interface IMoves
    {
        Moves Move { get; set; }

        void ReadInput();
        void ClearMove();
    }
}