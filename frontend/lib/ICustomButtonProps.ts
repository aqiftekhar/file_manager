export interface ICustomButtonProps {
  isLoading: boolean;
  className?: string;
  onclick: () => void;
  children?: React.ReactNode;
}
