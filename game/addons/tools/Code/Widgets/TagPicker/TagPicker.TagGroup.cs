namespace Editor;

partial class TagPicker
{
	class TagGroup : Widget
	{
		public string Title { get; private set; }
		public Checkbox Checkbox { get; private set; }

		public TagGroup( string title, bool showCheckbox ) : base( null )
		{
			Title = title;

			Layout = Layout.Column();

			if ( showCheckbox )
			{
				Checkbox = Layout.Add( new Checkbox( Title ) );
				Checkbox.StateChanged = OnCheckboxStateChanged;
			}
		}

		public void ResetCheckboxVisual()
		{
			if ( Checkbox == null ) return;

			// Temporarily disconnect to avoid toggling options while resetting.
			var handler = Checkbox.StateChanged;
			Checkbox.StateChanged = null;
			Checkbox.State = CheckState.Off;
			Checkbox.StateChanged = handler;
		}

		private void OnCheckboxStateChanged( CheckState state )
		{
			foreach ( var child in Children )
			{
				if ( child is TagOption option )
				{
					option.MouseLeftPress();
					option.Update();
				}
			}

			Update();
		}

		public TagOption Add( TagOption option )
		{
			Layout.Add( option );
			option.Parent = this;

			return option;
		}
	}
}
