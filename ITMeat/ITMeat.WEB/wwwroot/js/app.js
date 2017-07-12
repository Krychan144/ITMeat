$(".ui.sidebar.left").sidebar("setting", {
    transition: "overlay"
});

var clock = $('.your-clock').FlipClock({
    // ... your options here
    clockFace: 'TwentyFourHourClock',
});

$('.ui.calendar').calendar({
    type: 'date/time',
});