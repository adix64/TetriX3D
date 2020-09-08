# Welcome to **TetriX3D**!
<p>
<img src="https://raw.githubusercontent.com/adix64/TetriX3D/master/tetris.gif" alt="gif" width="360" align="left"/>
</p>  
This application uses a Model-View-Controller design.<br/><br/>
The Model component is implemented as a non-MonoBehavior class in TetrisCore.cs . This contains all game logic and makes use of the TetrisPiece(.cs) class which implements basic CW and CCW rotation functions. No additional space is needed for the rotations, which are done in-place by cycling through matrix borders. See this in TetrisPiece.cs . <br/><br/>
Everything is allocated at the program's initialization, so no heap allocation at runtime. This is because we know the maximum space required for the system so we can optimize.
TetrisCore.cs also emits signals for the View and Controller components. For signals, I used the EventSystem implemented in AppEventSystem.cs with events defined in AppEvents.cs . 
<br/><br/>
The partial TetrisGame class which inherits from MonoBehavior unifies all MVC components in TetrisClass.cs , TetrisClass.BoardDisplay.cs (which handles visuals) and TetrisGame.UserControl.cs(which alongside TetrisButton.cs and GameUI.cs implements the Controller component) . <br/><br/>
The View Component is implemented in TetrisClass.BoardDisplay.cs and NextPieceDisplay.cs . The pieces are read from the GameCore object and then displayed in the scene by material assignment and transform operations in TetrisClass.BoardDisplay.cs.<br/><br/>
You can create your own shapes using prefabs in Assets/Prefabs and usem by pluggin them in the template array in Inspector of TetrisGameGrid.
This functionality can be seen in TetrisGame.TemplateImporter.cs