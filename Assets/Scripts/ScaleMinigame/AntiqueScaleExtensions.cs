/* 
 * One place to set mass reduction amount accross scripts
*/

public static class AntiqueScaleExtensions
{
    public static float massReductionAmount = 100;
    // constructor called once automatically
    static AntiqueScaleExtensions()
    {

    }

    public static float ConvertRealWeightToUnityMass(int ItemSOWeight)
    {
        float amount = ItemSOWeight / massReductionAmount;
        return amount;
    }
}
