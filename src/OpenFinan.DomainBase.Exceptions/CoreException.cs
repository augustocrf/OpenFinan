using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace OpenFinan.DomainBase.Exceptions;

public abstract class CoreException : Exception
{
    protected CoreException(string message)
        : base(message)
    {

    }

    protected CoreException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public abstract string Key { get; }

    protected ICollection<CoreError> errors = new List<CoreError>();

    public IEnumerable<CoreError> Errors { get { return errors; } }
}

public abstract class CoreException<T> : CoreException
    where T : CoreError
{
    protected CoreException()
        : base("Ocorreu um erro de negócio, verifique a propriedade 'errors' para obter detalhes.")
    {

    }

    protected CoreException(string message)
        : base(message)
    {

    }

    protected CoreException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public CoreException AddError(params T[] errors)
    {
        foreach (var error in errors)
        {
            this.errors.Add(error);
        }

        return this;
    }
}