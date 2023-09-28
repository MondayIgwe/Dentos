namespace Elite3E.RestServices.Models.RequestModels
{
    public struct ValueUnion
    {
        public bool? Bool;
        public ValueClass ValueClass;
        public string String;

        public static implicit operator ValueUnion(bool Bool) => new() { Bool = Bool };
        public static implicit operator ValueUnion(string String) => new() { String = String };
        public static implicit operator ValueUnion(int value) => new() { String = value.ToString() };
        public static implicit operator ValueUnion(ValueClass ValueClass) => new() { ValueClass = ValueClass };
    }
}
