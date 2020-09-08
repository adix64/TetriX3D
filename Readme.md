# Welcome to **TetriX3D**!
<p>
<img src="https://raw.githubusercontent.com/adix64/TetriX3D/master/tetris.gif" alt="gif" width="360" align="left"/>
</p>  
This application uses the Model-View-Controller design.<br/>
The Model component is implemented as a non-MonoBehavior class in TetrisCore.cs . This contains all game logic and makes use of the TetrisPiece(.cs) class which implements basic CW and CCW rotation functions. No additional space is needed for the rotations, which are done in-place by cycling through matrix borders. See this in TetrisPiece.cs . <br/>
TetrisCore.cs also emits signals for the View and Controller components. For signals, I used the EventSystem implemented in AppEventSystem.cs with events defined in AppEvents.cs . 
<br/>
The partial TetrisGame class unifies all MVC components in TetrisClass.cs,
TetrisClass.BoardDisplay.cs (which handles visuals) and TetrisGame.BoardDisplay.cs .
