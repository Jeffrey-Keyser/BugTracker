_onkeyup: function(ev) {
    var k = ev.keyCode ? ev.keyCode :
        ev.rawEvent.keyCode;
    if (k != Sys.UI.Key.Tab) {
        this._timer.set_enabled(true);
    }
},

_onkeydown: function(ev) {
    this._timer.set_enabled(false);
},

_onTimerTick: function(sender, eventArgs) {
    this._timer.set_enabled(false);

    if (this._text != this.get_element().value) {
        this._text = this.get_element().value;

        this.get_element().onchange();
    }
},