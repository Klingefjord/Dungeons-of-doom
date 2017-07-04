namespace DungeonsOfDoom
{
    class StaminaPotion : Potion
    {
        public StaminaPotion(int weight, int strength) : base("Stamina Potion", weight, strength)
        {

        }
        public override int StaminaBuff { get => this.Strength; set => this.Strength = value; }
    }
}