// export function moreimg(){		



	// layui.use('upload', function(){
	//   var $ = layui.jquery
	//   ,upload = layui.upload;
	//   //多图片上传
	//   upload.render({
	//     elem: '#moreimg'
	//     ,url: 'https://httpbin.org/post' //改成您自己的上传接口
	//     ,multiple: true
	// 	,number:6
	// 	,auto:false
	// 	,bindAction:'#sumbit'
	//     ,before: function(obj){
	//       //预读本地文件示例，不支持ie8
	//       obj.preview(function(index, file, result){
	// 		  console.log(result);
	//         $('#demo2').append('<img   src="'+ result +'" alt="'+ file.name +'" class="layui-upload-img"'+' style="width: 80px; height: 80px; margin: 0 10px 10px 0;" '+'>')
	//       });
	//     }
	//     ,done: function(res){
	//       //上传完毕
	//     }
	//   });
	// });
	// 		}
	export function moreimg() {
	layui.use(['form', 'layedit'], function(){
	  var form = layui.form
	  ,layer = layui.layer
	  ,layedit = layui.layedit
	  //创建一个编辑器
	  var editIndex = layedit.build('demo',{
		tool: [
		  'strong' //加粗
		  ,'italic' //斜体
		  ,'underline' //下划线
		  ,'del' //删除线
		  ,'|' //分割线
		  ,'left' //左对齐
		  ,'center' //居中对齐
		  ,'right' //右对齐
		  ,'face' //表情
		],height:100,
	  });
	 
	  //自定义验证规则
	  form.verify({
	    content: function(value){
	     layedit.sync(editIndex);
	    },
	  });
	  //监听提交
	  form.on('submit(sure)', function(data){
		  console.log(JSON.stringify(data.field.file));
	    // layer.alert(JSON.stringify(data.field), {
	    //   title: '最终的提交信息'
	    // })
	    return false;
	  });
	});

	layui.use('upload', function(){
	  var $ = layui.jquery
	  ,upload = layui.upload;
	  //多图片上传
	  upload.render({
	    elem: '#moreimg'
	    ,url: "http://39.101.180.27:8089/api/UpLoad/DynamicImgsUpLoad" //改成您自己的上传接口
	    ,multiple: true
		,number:6
		,auto:true
		// ,bindAction:'#sure'
	    ,before: function(obj){
	      //预读本地文件示例，不支持ie8
	      obj.preview(function(index, file, result){
			  // console.log(result);
	        $('#demo2').append('<img   src="'+ result +'" alt="'+ file.name +'" class="layui-upload-img"'+' style="width: 80px; height: 80px; margin: 0 10px 10px 0;" '+'>')
	      });
	    }
	    ,done: function(res){
	      console.log(res);
	    }
	  });
	});

// ////////////////////
	layui.use('form', function() {
				var form = layui.form;
				//监听提交
				form.on('submit(formDemo)', function(data) {
					layer.msg(JSON.stringify(data.field));
					return false;
				});
			});

	}
