// 折叠
export function tab (){	
	layui.use(['element', 'layer'], function(){
	  var element = layui.element;
	  var layer = layui.layer;
	  
	  //监听折叠
	  element.on('collapse(test)', function(data){
	    layer.msg('展开状态：'+ data.show);
	  });
	});
}