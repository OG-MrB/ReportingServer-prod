function setupLabel() {
    if ($('.label_check').length) {
        $('.label_check').each(function(){ 
            $(this).removeClass('c_on');
        });
        $('.label_check :checked').each(function(){ 
            $(this).parent('label').addClass('c_on');
        });                
    };
    if ($('.label_radio').length) {
        $('.label_radio').each(function(){ 
            $(this).removeClass('r_on');
        });
        $('.label_radio :checked').each(function(){ 
            $(this).parent('label').addClass('r_on');
        });
    };
};
$(document).ready(function(){
    $('body').addClass('has-js');
    $('.label_check, .label_radio').click(function(){
        setupLabel();
    });
    setupLabel(); 
});
