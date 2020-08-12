	export function upimgjs(){		
	// 更换用户头像
	layui.use(["upload"], function() {
	  // 图片上传
	  layui.use("upload", function() {
	    var that = this;
	    var $ = layui.jquery,
	      upload = layui.upload;
	    //普通图片上传
	    var uploadInst = upload.render({
	      elem: "#upimg",
	      url: "http://39.101.180.27:8089/api/UpLoad/UserPhotoUpLoad", //改成您自己的上传接口
	      data: {
	        id: function() {
	          return sessionStorage.U_Id
	        }
	      },
	      before: function(obj) {
	        //预读本地文件示例，不支持ie8
	        obj.preview(function(index, file, result) {
	          $("#imgs").attr("src", result); //图片链接（base64）
	        });
	      },
	      done: function(res) {
	        //如果上传失败
	        if (res.code > 0) {
	          return layer.msg("上传失败");
	        }
	        // console.log(res);
	        //上传成功
	        layer.msg("图片上传成功");
			sessionStorage.setItem('U_Img',res);
			window.location.reload()
	      },
	      error: function() {
	        //演示失败状态，并实现重传
	        var demoText = $("#demoText");
	        demoText.html(
	          '<span style="color: #FF5722;">上传失败</span> <a class="layui-btn layui-btn-xs demo-reload">重试</a>'
	        );
	        demoText.find(".demo-reload").on("click", function() {
	          uploadInst.upload();
	        });
	      }
	    });
	  });
	});
	
			}
			