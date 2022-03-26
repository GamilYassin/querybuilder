using System;

namespace SqlKata
{
    public class Join : QueryBase<Join>
    {
        #region Fields

        protected string _type = "inner join";

        #endregion Fields

        #region Constructors

        public Join( ) : base( )
        {
        }

        #endregion Constructors

        #region Properties

        public string Type
        {
            get
            {
                return _type;
            }
            set
            {
                _type = value.ToUpperInvariant( );
            }
        }

        #endregion Properties

        #region Methods

        public Join AsCross( ) => AsType( "cross join" );

        public Join AsInner( ) => AsType( "inner join" );

        public Join AsLeft( ) => AsType( "left join" );

        public Join AsOuter( ) => AsType( "outer join" );

        public Join AsRight( ) => AsType( "right join" );

        public Join AsType( string type )
        {
            Type = type;
            return this;
        }

        public override Join Clone( )
        {
            var clone = base.Clone( );
            clone._type = _type;
            return clone;
        }

        /// <summary>
        ///  Alias for "from" operator. Since "from" does not sound well with join clauses
        /// </summary>
        /// <param name="table">
        /// </param>
        /// <returns>
        /// </returns>
        public Join JoinWith( string table ) => From( table );

        public Join JoinWith( Query query ) => From( query );

        public Join JoinWith( Func<Query, Query> callback ) => From( callback );

        public override Join NewQuery( )
        {
            return new Join( );
        }

        public Join On( string first, string second, string op = "=" )
        {
            return AddComponent( "where", new TwoColumnsCondition
            {
                First = first,
                Second = second,
                Operator = op,
                IsOr = GetOr( ),
                IsNot = GetNot( )
            } );
        }

        public Join OrOn( string first, string second, string op = "=" )
        {
            return Or( ).On( first, second, op );
        }

        #endregion Methods
    }
}
