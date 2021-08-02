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

  var clicks = 0;
  var clickss = 0;

function onClick() {
  clicks += 1;
  document.getElementById("clicks").innerHTML = clicks;
};

function onClickk() {
if (clicks>0){
    clicks -= 1;
    document.getElementById("clicks").innerHTML = clicks;
    
  }
};



function Clickon() {
  clickss += 1;
  document.getElementById("clickss").innerHTML = clickss;
};
function Clickkon() {
  if(clickss>0){
    clickss -= 1;
  document.getElementById("clickss").innerHTML = clickss;
  }
  
};
