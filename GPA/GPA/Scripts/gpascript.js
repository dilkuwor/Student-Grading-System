
function hideDiv() {
    debugger;
    if (document.getElementById) { // DOM3 = IE5, NS6 
        
        document.getElementById('feedbackDiv').style.visibility = 'collapse';
    } 
    else { 
        if (document.layers) { // Netscape 4 
            document.hideShow.visibility = 'hidden'; 
        } 
        else { // IE 4 
            document.all.hideShow.style.visibility = 'hidden'; 
        } 
    } 
}

function showDiv() {
    debugger;
    
    if ($('#btnFeedback').hasClass('active')) {
        hideDiv();
        $('#btnFeedback').removeClass('active')
        return;
    }
    else {
        $('#btnFeedback').addClass('active');
    }
    if (document.getElementById) { // DOM3 = IE5, NS6 
        
       
        document.getElementById('feedbackDiv').style.display = 'block';
        document.getElementById('feedbackDiv').style.visibility = 'visible';
       
    } 
    else { 
        if (document.layers) { // Netscape 4 
            document.hideShow.visibility = 'visible'; 
        } 
        else { // IE 4 
            document.all.hideShow.style.visibility = 'visible'; 
        } 
    } 
} 


function dil() {
    debugger;
    alert('hi dil');
}