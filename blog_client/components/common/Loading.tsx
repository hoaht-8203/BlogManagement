import { Loader2 } from 'lucide-react';

const Loading = () => {
  return (
    <div className="flex h-[214px] items-center justify-center">
      <div className="flex flex-col items-center justify-center">
        <Loader2 className="size-10 animate-spin" />
        <h1 className="text-2xl font-bold">Loading</h1>
        <p className="text-muted-foreground">Please wait while we load the page</p>
      </div>
    </div>
  );
};

export default Loading;
