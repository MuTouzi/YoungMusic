	export function layuijs(){			
	// layui组件
			layui.use(['form', 'layedit', 'laydate'], function() {
				var form = layui.form,
					layedit = layui.layedit,
					laydate = layui.laydate;
					 $ = layui.$;

				//日期
				laydate.render({
					elem: '#date',
					showBottom: false
				});
			});
			}