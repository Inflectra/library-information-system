function vnb(){}
function pnb(){}
function dYb(){}
function HFc(){}
function mYb(b){qEb(b.c,_Xb(b.b))}
function vEc(b,c){return DEc(b,c,b.b.c)}
function SEc(){UEc.call(this,false,vFc())}
function DFc(b,c){EFc.call(this,b,false);AFc(this,c)}
function CFc(b,c){EFc.call(this,b,false);this.c=c}
function BFc(b){EFc.call(this,$Nd,true);AFc(this,b)}
function gYb(b){this.d=b;this.c=oGb(this.d.b)}
function unb(){var b;while(qnb){b=qnb;qnb=qnb.c;!qnb&&(rnb=null);mYb(b.b)}}
function DEc(b,c,d){if(d<0||d>b.b.c){throw new s_c}b.p&&(c.N[$Jd]=2,undefined);uEc(b,d,c.N);S6c(b.b,d,c);return c}
function lGb(b){var c,d;c=D0(b.b.Tc(BNd),54);if(c==null){d=o0(Iib,{19:1,54:1},1,[CNd,DNd,ENd]);b.b.Vc(BNd,d);return d}else{return c}}
function nGb(b){var c,d;c=D0(b.b.Tc(JNd),54);if(c==null){d=o0(Iib,{19:1,54:1},1,[KNd,LNd,MNd]);b.b.Vc(JNd,d);return d}else{return c}}
function mGb(b){var c,d;c=D0(b.b.Tc(FNd),54);if(c==null){d=o0(Iib,{19:1,54:1},1,[GNd,HNd,And,INd]);b.b.Vc(FNd,d);return d}else{return c}}
function jGb(b){var c,d;c=D0(b.b.Tc(pNd),54);if(c==null){d=o0(Iib,{19:1,54:1},1,[qNd,rNd,sNd,tNd,uNd]);b.b.Vc(pNd,d);return d}else{return c}}
function kGb(b){var c,d;c=D0(b.b.Tc(vNd),54);if(c==null){d=o0(Iib,{19:1,54:1},1,[wNd,xNd,yNd,zNd,ANd]);b.b.Vc(vNd,d);return d}else{return c}}
function oGb(b){var c,d;c=D0(b.b.Tc(NNd),54);if(c==null){d=o0(Iib,{19:1,54:1},1,[ONd,PNd,QNd,RNd,SNd,TNd]);b.b.Vc(NNd,d);return d}else{return c}}
function xnb(){snb=new vnb;RB((OB(),NB),17);!!$stats&&$stats(IC(oNd,ogd,-1,-1));snb.od();!!$stats&&$stats(IC(oNd,yDd,-1,-1))}
function JFc(){var b;this.N=$doc.createElement(dsd);this.N[Tfd]=_Nd;b=$doc.createElement(agd);this.N.appendChild(b);b[Tfd]=aOd}
function AFc(b,c){b.e=c;!!b.d&&REc(b.d,b);if(c){(fAc(),c.N).tabIndex=-1;b.N.setAttribute(ZNd,Npd)}else{b.N.setAttribute(ZNd,wsd)}}
function _Xb(b){var c,d,e,f,g,i,j,k,n,o,p,q,r;p=new gYb(b);o=new SEc;o.c=true;o.N.style[pkd]=UNd;o.f=true;r=new TEc(true);q=lGb(b.b);for(n=0;n<q.length;++n){tEc(r,new CFc(q[n],p))}e=new TEc(true);e.f=true;tEc(o,new DFc(VNd,e));f=kGb(b.b);for(n=0;n<f.length;++n){if(n==3){vEc(e,new JFc);tEc(e,new DFc(f[3],r));vEc(e,new JFc)}else{tEc(e,new CFc(f[n],p))}}c=new TEc(true);tEc(o,new DFc(WNd,c));d=jGb(b.b);for(n=0;n<d.length;++n){tEc(c,new CFc(d[n],p))}g=new TEc(true);tEc(o,new BFc(g));i=mGb(b.b);for(n=0;n<i.length;++n){tEc(g,new CFc(i[n],p))}j=new TEc(true);vEc(o,new JFc);tEc(o,new DFc(XNd,j));k=nGb(b.b);for(n=0;n<k.length;++n){tEc(j,new CFc(k[n],p))}cPc(o.N,egd,YNd);QEc(o,YNd);return o}
var PNd='A fine selection indeed',MNd='About GWT',TNd='Another wasted click',bOd='AsyncLoader17',KNd='Contents',tNd='Copy',sNd='Cut',cOd='CwMenuBar$1',QNd="Don't you have anything better to do than select menu items?",GNd='Download',WNd='Edit',HNd='Examples',ANd='Exit',VNd='File',CNd='Fishing in the desert.txt',LNd='Fortune Cookie',$Nd='GWT',INd="GWT wit' the program",XNd='Help',DNd='How to tame a wild parrot',ENd='Idiots Guide to Emu Farms',dOd='MenuItemSeparator',wNd='New',xNd='Open',uNd='Paste',rNd='Redo',ONd='Thank you for selecting a menu item',RNd='Try something else',qNd='Undo',ZNd='aria-haspopup',YNd='cwMenuBar',pNd='cwMenuBarEditOptions',vNd='cwMenuBarFileOptions',BNd='cwMenuBarFileRecents',FNd='cwMenuBarGWTOptions',JNd='cwMenuBarHelpOptions',NNd='cwMenuBarPrompts',_Nd='gwt-MenuItemSeparator',aOd='menuSeparatorInner',oNd='runCallbacks17',SNd='this is just a menu!';_=vnb.prototype=pnb.prototype=new kr;_.gC=function wnb(){return u4};_.od=function Anb(){unb()};_.cM={};_=gYb.prototype=dYb.prototype=new kr;_.Yb=function hYb(){$wnd.alert(this.c[this.b]);this.b=(this.b+1)%this.c.length};_.gC=function iYb(){return x9};_.cM={51:1,110:1};_.b=0;_.d=null;_=SEc.prototype=qEc.prototype;_=DFc.prototype=CFc.prototype=BFc.prototype=wFc.prototype;_=JFc.prototype=HFc.prototype=new Cu;_.gC=function KFc(){return qeb};_.cM={83:1,119:1};var u4=J$c(Wvd,bOd),x9=J$c(Fyd,cOd),qeb=J$c(ztd,dOd);$entry(xnb)();