			export function top(){
				//锚点回到顶部1
				layui.use('util', function() {
					var util = layui.util;
					//执行
					util.fixbar({
						bar1: false,
						click: function(type) {
							console.log(type);
							if (type === 'bar1') {
								// alert('点击了bar1')
							}
						}
					});
				});
				
				
				// 登录页面的拖拉1
				$(".l-top").mousedown(function() {
					var div = $(".login").get(0)
					// alert("a")
					var X = event.clientX - div.offsetLeft;
					var Y = event.clientY - div.offsetTop;
					document.onmousemove = function() {
						div.style.left = event.clientX - X + "px";
						div.style.top = event.clientY - Y + "px";
				
						if (div.offsetLeft < div.offsetWidth / 2) {
							div.style.left = div.offsetWidth / 2 + "px";
						}
						if (div.offsetLeft > document.documentElement.clientWidth - div.offsetWidth / 2) {
							div.style.left = document.documentElement.clientWidth - div.offsetWidth / 2 + "px";
						}
						if (div.offsetTop < div.offsetHeight / 2) {
							div.style.top = div.offsetHeight / 2 + "px";
						}
						if (div.offsetTop > document.documentElement.clientHeight - div.offsetHeight / 2) {
							div.style.top = document.documentElement.clientHeight - div.offsetHeight / 2 + "px";
						}
					}
					document.onmouseup = function() {
						document.onmousemove = null;
						document.onmouseup = null;
					}
					// 注册页面的拖拉1
					$(".r-top").mousedown(function() {
						var div = $(".register").get(0)
						// alert("a")
						var X = event.clientX - div.offsetLeft;
						var Y = event.clientY - div.offsetTop;
				
						document.onmousemove = function() {
							div.style.left = event.clientX - X + "px";
							div.style.top = event.clientY - Y + "px";
				
							if (div.offsetLeft < div.offsetWidth / 2) {
								div.style.left = div.offsetWidth / 2 + "px";
							}
							if (div.offsetLeft > document.documentElement.clientWidth - div.offsetWidth / 2) {
								div.style.left = document.documentElement.clientWidth - div.offsetWidth / 2 + "px";
							}
							if (div.offsetTop < div.offsetHeight / 2) {
								div.style.top = div.offsetHeight / 2 + "px";
							}
							if (div.offsetTop > document.documentElement.clientHeight - div.offsetHeight / 2) {
								div.style.top = document.documentElement.clientHeight - div.offsetHeight / 2 + "px";
							}
						}
						document.onmouseup = function() {
							document.onmousemove = null;
							document.onmouseup = null;
						}
					})
				})
				
			}