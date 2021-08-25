/*
Inspired by Florin Pop's coding challenges, you can check them here: https://www.florin-pop.com/blog/2019/03/weekly-coding-challenge/
*/

function loading() {
    document.querySelectorAll(".bar").forEach(function(current) {
      let startWidth = 0;
      const endWidth = current.dataset.size;
      
      /* 
      setInterval() time sholud be set as trasition time / 100. 
      In our case, 2 seconds / 100 = 20 milliseconds. 
      */
      const interval = setInterval(frame, 20);
  
      function frame() {
        if (startWidth >= endWidth) {
          clearInterval(interval);
        } else {
            startWidth++;
            current.style.width = `${endWidth}%`;
            current.firstElementChild.innerText = `${startWidth}%`;
          }
       }
    });
  }
  
  setTimeout(loading, 1000);

var clickss = $("#clickss").attr('value');
var clicks = parseInt($("#clicks").attr('value'), 10);

function onClick(id) {
  clicks += 1;
    $("#clicks").val(clicks);
};

function onClickk(id) {
if (clicks>1){
    clicks -= 1;
    $("#clicks").val(clicks);

    
  }
};



function Clickon() {
  clickss += 1;
    $("#clickss").val(clickss);
};
function Clickkon() {
  if(clickss>1){
    clickss -= 1;
      $("#clickss").val(clickss);
  }
  
};
