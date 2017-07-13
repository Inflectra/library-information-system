/*
 * Ext JS Library 2.0.2
 * Copyright(c) 2006-2008, Ext JS, LLC.
 * licensing@extjs.com
 * 
 * http://extjs.com/license
 */

Ext.dd.ScrollManager=function(){var C=Ext.dd.DragDropMgr;var E={};var B=null;var H={};var G=function(K){B=null;A()};var I=function(){if(C.dragCurrent){C.refreshCache(C.dragCurrent.groups)}};var D=function(){if(C.dragCurrent){var K=Ext.dd.ScrollManager;var L=H.el.ddScrollConfig?H.el.ddScrollConfig.increment:K.increment;if(!K.animate){if(H.el.scroll(H.dir,L)){I()}}else{H.el.scroll(H.dir,L,true,K.animDuration,I)}}};var A=function(){if(H.id){clearInterval(H.id)}H.id=0;H.el=null;H.dir=""};var F=function(L,K){A();H.el=L;H.dir=K;H.id=setInterval(D,Ext.dd.ScrollManager.frequency)};var J=function(N,P){if(P||!C.dragCurrent){return }var Q=Ext.dd.ScrollManager;if(!B||B!=C.dragCurrent){B=C.dragCurrent;Q.refreshCache()}var R=Ext.lib.Event.getXY(N);var S=new Ext.lib.Point(R[0],R[1]);for(var L in E){var M=E[L],K=M._region;var O=M.ddScrollConfig?M.ddScrollConfig:Q;if(K&&K.contains(S)&&M.isScrollable()){if(K.bottom-S.y<=O.vthresh){if(H.el!=M){F(M,"down")}return }else{if(K.right-S.x<=O.hthresh){if(H.el!=M){F(M,"left")}return }else{if(S.y-K.top<=O.vthresh){if(H.el!=M){F(M,"up")}return }else{if(S.x-K.left<=O.hthresh){if(H.el!=M){F(M,"right")}return }}}}}}A()};C.fireEvents=C.fireEvents.createSequence(J,C);C.stopDrag=C.stopDrag.createSequence(G,C);return{register:function(M){if(Ext.isArray(M)){for(var L=0,K=M.length;L<K;L++){this.register(M[L])}}else{M=Ext.get(M);E[M.id]=M}},unregister:function(M){if(Ext.isArray(M)){for(var L=0,K=M.length;L<K;L++){this.unregister(M[L])}}else{M=Ext.get(M);delete E[M.id]}},vthresh:25,hthresh:25,increment:100,frequency:500,animate:true,animDuration:0.4,refreshCache:function(){for(var K in E){if(typeof E[K]=="object"){E[K]._region=E[K].getRegion()}}}}}();