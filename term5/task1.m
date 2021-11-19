function [] = task1(func)
x = -1:0.5:1;
y = func(x);
n = 5;
q = 3;
alpha = ls(x, y, n, q);
plotls(x, y, n, alpha, q);

end

function alpha=ls(x,y,n,q) 

b=zeros(n,1); 
for i=1:n 
    b(i)=y(i); 
    for j=1:q+1 
        A(i,j)=x(i)^(q+1-j); 
    end
end
C=A'*A;
d=A'*b;
alpha=C\d;

end
    
function plotls(x,y,n,alpha,q) 
    dx=(x(n)-x(1))/100;
    for j=1:101 
        xx(j)=x(1)+dx*(j-1); 
        yy(j)=0; 
        for k=1:q+1 
            yy(j)=yy(j)+alpha(k)*xx(j)^(q+1-k); 
        end
    end
    plot(xx,yy);
end