namespace TPCadastroUsuario.Core.Entities;

public abstract class EntidadeBase<TId> 
{
    public TId Id { get; protected set; }

    protected EntidadeBase() { }

    protected EntidadeBase(TId id)
    {
        Id = id;
    }

    public override bool Equals(object obj)
    {
        if (obj is not EntidadeBase<TId> other) return false;
        if (ReferenceEquals(this, other)) return true;
        if (GetType() != other.GetType()) return false;
        return EqualityComparer<TId>.Default.Equals(Id, other.Id);
    }
    public override int GetHashCode() => HashCode.Combine(Id);
}
