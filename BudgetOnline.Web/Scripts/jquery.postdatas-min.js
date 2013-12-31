(function(a){a.postdatas=function(b){var c=a.extend({url:"",datas:[]},b);
var d=a("<form>");
d.attr({action:c.url,method:"POST"}).css({display:"none"});
for(i=0;
i<c.datas.length;
i++){a('<input type="hidden" name="'+c.datas[i].name+'" value="'+c.datas[i].value+'" />').appendTo(d)
}d.appendTo("body");
d.submit()
}
})(jQuery);